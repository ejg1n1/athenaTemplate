using System.Reflection;
using Application.Exceptions;
using Core.Attributes;
using Microsoft.AspNetCore.JsonPatch;

namespace Application.Extensions;

public static class JsonPatchDocumentExtensions
{
    public static void CheckAttributesThenApply<T>(this JsonPatchDocument<T> patchDoc,
        T objectToApplyTo,
        Action<JsonPatchError> logErrorAction,
        List<string> currentUserRoles)
        where T : class
    {
        if (patchDoc == null) throw new ArgumentNullException(nameof(patchDoc));
        if (objectToApplyTo == null) throw new ArgumentNullException(nameof(objectToApplyTo));
        if (logErrorAction == null) throw new ArgumentNullException(nameof(logErrorAction));
        var objectTypeToPatch = objectToApplyTo.GetType();

        foreach (var op in patchDoc.Operations)
        {
            if (!string.IsNullOrWhiteSpace(op.path))
            {
                var pathToPatch = op.path.Trim('/').ToLowerInvariant();
                var attributesFilter = BindingFlags.Public | BindingFlags.FlattenHierarchy | BindingFlags.Instance;
                var propertyToPatch = typeof(T).GetProperties(attributesFilter).FirstOrDefault(p
                    => p.Name.Equals(pathToPatch, StringComparison.InvariantCultureIgnoreCase));

                var patchRestrictedToRolesAttribute = propertyToPatch?
                    .GetCustomAttributes(typeof(PatchRestrictedToRolesAttribute), false)
                    .Cast<PatchRestrictedToRolesAttribute>()
                    .SingleOrDefault();
                if (patchRestrictedToRolesAttribute != null)
                {
                    var userCanUpdateProperty = patchRestrictedToRolesAttribute.GetUserRoles().Any(r
                        => currentUserRoles != null &&
                           currentUserRoles.Any(c => c.Equals(r, StringComparison.InvariantCultureIgnoreCase)));

                    if (!userCanUpdateProperty)
                        throw new PatchNotAllowedException($"Current user role is not permitted to patch " +
                                                           $"{objectTypeToPatch.Name}.{propertyToPatch!.Name}");
                }

                var patchDisabledForProperty = propertyToPatch?
                    .GetCustomAttributes(typeof(PatchDisabledAttribute), false)
                    .SingleOrDefault();

                if (patchDisabledForProperty != null)
                    throw new PatchNotAllowedException($"Patch operations on {objectTypeToPatch.Name}" +
                                                       $".{propertyToPatch!.Name} have been disabled");
            }
        }
        
        patchDoc.ApplyTo(objectToApplyTo, logErrorAction);
    }
}