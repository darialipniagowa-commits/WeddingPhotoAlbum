namespace WeddingPhotoAlbum.Models;

public sealed class PhotoSlot
{
    public PhotoSlot(
        int id,
        string title,
        string layoutClass)
    {
        Id = id;
        Title = title;
        LayoutClass = layoutClass;
    }

    public int Id { get; }

    public string Title { get; }

    public string LayoutClass { get; }

    public string AccessibleLabel =>
        $"Добавить фото: {Title}";

    public string? FileName { get; set; }

    public string? ImageDataUrl { get; set; }

    public string? ErrorMessage { get; set; }

    public bool HasPhoto =>
        !string.IsNullOrWhiteSpace(ImageDataUrl);
}