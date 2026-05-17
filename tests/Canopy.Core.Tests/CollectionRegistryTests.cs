using Canopy.Core.Collections;
using Canopy.Core.Engine;

namespace Canopy.Core.Tests;

public class CollectionRegistryTests
{
    [Canopy.Core.Attributes.Collection("articles")]
    public class Article : CanopyDocument { }

    [Canopy.Core.Attributes.Collection("posts", Label = "Blog Posts")]
    public class BlogPost : CanopyDocument { }

    public class UndecoratedDoc : CanopyDocument { }

    [Fact]
    public void Can_register_a_collection()
    {
        var registry = new CollectionRegistry();
        registry.Register(typeof(Article).Assembly);
        registry.Initialize();

        Assert.True(registry.Collections.ContainsKey("articles"));
    }

    [Fact]
    public void Register_discovers_all_collections_in_assembly()
    {
        var registry = new CollectionRegistry();
        registry.Register(typeof(Article).Assembly);
        registry.Initialize();

        Assert.True(registry.Collections.ContainsKey("articles"));
        Assert.True(registry.Collections.ContainsKey("posts"));
    }

    [Fact]
    public void Register_sets_label_to_class_name_when_not_set()
    {
        var registry = new CollectionRegistry();
        registry.Register(typeof(Article).Assembly);
        registry.Initialize();

        var meta = registry.Collections["articles"];
        Assert.Equal("Article", meta.Label);
    }

    [Fact]
    public void Register_uses_label_from_attribute_when_set()
    {
        var registry = new CollectionRegistry();
        registry.Register(typeof(BlogPost).Assembly);
        registry.Initialize();

        var meta = registry.Collections["posts"];
        Assert.Equal("Blog Posts", meta.Label);
    }

    [Fact]
    public void Register_sets_clr_type()
    {
        var registry = new CollectionRegistry();
        registry.Register(typeof(Article).Assembly);
        registry.Initialize();

        Assert.Equal(typeof(Article), registry.Collections["articles"].ClrType);
    }

    [Fact]
    public void Register_ignores_undecorated_document_types()
    {
        var registry = new CollectionRegistry();
        registry.Register(typeof(UndecoratedDoc).Assembly);
        registry.Initialize();

        Assert.DoesNotContain(registry.Collections.Values, m => m.ClrType == typeof(UndecoratedDoc));
    }

    [Fact]
    public void Register_duplicate_slug_throws()
    {
        var registry = new CollectionRegistry();
        registry.Register(typeof(Article).Assembly);

        Assert.Throws<InvalidOperationException>(() =>
            registry.Register(typeof(Article).Assembly));
    }

    [Fact]
    public void Register_after_initialize_throws()
    {
        var registry = new CollectionRegistry();
        registry.Initialize();

        Assert.Throws<InvalidOperationException>(() =>
            registry.Register(typeof(Article).Assembly));
    }

    [Fact]
    public void Collections_before_initialize_throws()
    {
        var registry = new CollectionRegistry();
        registry.Register(typeof(Article).Assembly);

        Assert.Throws<InvalidOperationException>(() => _ = registry.Collections);
    }
}
