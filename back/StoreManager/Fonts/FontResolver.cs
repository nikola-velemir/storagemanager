using PdfSharp.Fonts;

namespace StoreManager.Fonts;

public class FontResolver : IFontResolver
{
    private readonly byte[] _fontData;

    public FontResolver()
    {
        var fontPath = Path.Combine(Directory.GetCurrentDirectory(),"Fonts","Helvetica.ttf");
        _fontData = File.ReadAllBytes(fontPath);
    }
    public FontResolverInfo? ResolveTypeface(string familyName, bool bold, bool italic)
    {
        return new FontResolverInfo("Helvetica#");
    }

    public byte[]? GetFont(string faceName)
    {
        return _fontData;
    }
    public string DefaultFontName => "Helvetica";
}