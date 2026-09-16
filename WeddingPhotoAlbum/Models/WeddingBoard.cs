namespace WeddingPhotoAlbum.Models;

public sealed class WeddingBoard
{
    public string CoupleNames { get; init; } = string.Empty;

    public string WeddingDate { get; init; } = string.Empty;

    public string BoardTitle { get; init; } = string.Empty;

    public string BoardDescription { get; init; } = string.Empty;

    public List<PhotoSlot> Slots { get; init; } = [];

    public int CompletedPhotoCount =>
        Slots.Count(slot => slot.HasPhoto);

    public static WeddingBoard CreateMainBoard()
    {
        return new WeddingBoard
        {
            CoupleNames = "Дарья и Янис",
            WeddingDate = "19.09.2026",
            BoardTitle = "Свадебный фотоквест",
            BoardDescription =
                "Нажмите на рамку, чтобы добавить фотографию.",

            Slots =
            [
                new PhotoSlot(
                    1,
                    "С молодожёнами",
                    "photo-slot-landscape"),

                new PhotoSlot(
                    2,
                    "Случайный кадр",
                    "photo-slot-portrait"),

                new PhotoSlot(
                    3,
                    "Весёлый момент",
                    "photo-slot-landscape"),

                new PhotoSlot(
                    4,
                    "Все девочки",
                    "photo-slot-portrait"),

                new PhotoSlot(
                    5,
                    "Тёплые объятия",
                    "photo-slot-landscape"),

                new PhotoSlot(
                    6,
                    "Все мальчики",
                    "photo-slot-portrait"),

                new PhotoSlot(
                    7,
                    "Свидетели",
                    "photo-slot-landscape"),

                new PhotoSlot(
                    8,
                    "Тост",
                    "photo-slot-portrait"),

                new PhotoSlot(
                    9,
                    "Танец",
                    "photo-slot-landscape"),

                new PhotoSlot(
                    10,
                    "С родителями",
                    "photo-slot-portrait"),

                new PhotoSlot(
                    11,
                    "Все вместе",
                    "photo-slot-wide"),

                new PhotoSlot(
                    12,
                    "Две семьи",
                    "photo-slot-portrait")
            ]
        };
    }
}