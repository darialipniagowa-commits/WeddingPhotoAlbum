namespace WeddingPhotoAlbum.Models;

public sealed class BoardProgress
{
    public string BoardId { get; set; } = string.Empty;

    public DateTime SavedAtUtc { get; set; }

    public string? BoardTitle { get; set; }

    public string? BoardDescription { get; set; }

    public List<string> SelectedTagKeys { get; set; } = [];

    public List<SavedPhotoSlot> Slots { get; set; } = [];
}

public sealed class SavedPhotoSlot
{
    public int Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public string LayoutClass { get; set; } = string.Empty;

    public string? FileName { get; set; }

    public string? ImageDataUrl { get; set; }
}