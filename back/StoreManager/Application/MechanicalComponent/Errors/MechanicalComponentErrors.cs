using StoreManager.Application.Common;

namespace StoreManager.Application.MechanicalComponent.Errors;

public static class MechanicalComponentErrors
{
    public static Error ComponentIdParseError => new Error("ComponentIdParseError", 400, "Component Id Parse Error");
    public static Error ComponentNotFound => new Error("ComponentNotFound", 404, "Component Not Found");
}