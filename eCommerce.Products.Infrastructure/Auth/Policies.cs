namespace eCommerce.Products.Infrastructure.Auth;

public static class Policies
{
    public static class Products
    {
        public const string AllScopes = $"{Resources.Product}-{Scopes.AllScopes}";
        public const string View = $"{Resources.Product}-{Scopes.View}";
        public const string Add = $"{Resources.Product}-{Scopes.Add}";
        public const string Edit = $"{Resources.Product}-{Scopes.Edit}";
        public const string Delete = $"{Resources.Product}-{Scopes.Delete}";

        public static readonly string[] AllPolicies = new string[]
        {
            AllScopes,
            View,
            Add,
            Edit,
            Delete
        };
    }

    public static class Categories
    {
        public const string AllScopes = $"{Resources.Category}-{Scopes.AllScopes}";
        public const string View = $"{Resources.Category}-{Scopes.View}";
        public const string Add = $"{Resources.Category}-{Scopes.Add}";
        public const string Edit = $"{Resources.Category}-{Scopes.Edit}";
        public const string Delete = $"{Resources.Category}-{Scopes.Delete}";

        public static readonly string[] AllPolicies = new string[]
        {
            AllScopes,
            View,
            Add,
            Edit,
            Delete
        };
    }

    public static class ProductReviewes
    {
        public const string AllScopes = $"{Resources.ProductReview}-{Scopes.AllScopes}";
        public const string View = $"{Resources.ProductReview}-{Scopes.View}";
        public const string Add = $"{Resources.ProductReview}-{Scopes.Add}";
        public const string Edit = $"{Resources.ProductReview}-{Scopes.Edit}";
        public const string Delete = $"{Resources.ProductReview}-{Scopes.Delete}";

        public static readonly string[] AllPolicies = new string[]
        {
            AllScopes,
            View,
            Add,
            Edit,
            Delete
        };
    }

    public static HashSet<string> All()
    {
        var policies = new string[][]
        {
            Products.AllPolicies,
            Categories.AllPolicies,
            ProductReviewes.AllPolicies
        }
            .SelectMany(p => p)
            .Distinct()
            .ToHashSet();

        return policies;
    }
}
