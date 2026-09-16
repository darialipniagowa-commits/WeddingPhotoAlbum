namespace WeddingPhotoAlbum.Models;

public sealed class QuestTag
{
    public QuestTag(
        string key,
        string title,
        params string[] prompts)
    {
        Key = key;
        Title = title;
        Prompts = prompts;
    }

    public string Key { get; }

    public string Title { get; }

    public IReadOnlyList<string> Prompts { get; }
}

public static class QuestPromptCatalog
{
    private static readonly string[] LayoutClasses =
    [
        "photo-slot-landscape",
        "photo-slot-portrait",
        "photo-slot-landscape",

        "photo-slot-portrait",
        "photo-slot-landscape",
        "photo-slot-portrait",

        "photo-slot-landscape",
        "photo-slot-portrait",
        "photo-slot-landscape",

        "photo-slot-portrait",
        "photo-slot-wide",
        "photo-slot-portrait"
    ];

    private static readonly string[] UniversalPrompts =
    [
        "Самый неожиданный момент",
        "Случайный кадр",
        "Все вместе",
        "Самая красивая улыбка",
        "Тёплые объятия",
        "Фото с молодожёнами",
        "Самая весёлая компания",
        "Момент, который хочется запомнить",
        "Неожиданная деталь",
        "Фото без подготовки",
        "Лучший кадр вечера",
        "Счастливый момент"
    ];

    public static IReadOnlyList<QuestTag> Tags { get; } =
    [
        new(
            "romance",
            "Романтика",
            "Нежный взгляд",
            "Поцелуй",
            "Момент вдвоём",
            "Романтические объятия"),

        new(
            "dancing",
            "Танцы",
            "Первый танец",
            "Самый активный танцор",
            "Танец всей компанией",
            "Неожиданное движение"),

        new(
            "friends",
            "Друзья",
            "Селфи с друзьями",
            "Старые друзья",
            "Новые знакомства",
            "Самая весёлая компания"),

        new(
            "family",
            "Семья",
            "Две семьи",
            "С родителями",
            "Семейные объятия",
            "Разные поколения"),

        new(
            "funny",
            "Смешное",
            "Самая смешная гримаса",
            "Неожиданный кадр",
            "Все смеются",
            "Повторите позу молодожёнов"),

        new(
            "details",
            "Детали",
            "Обручальные кольца",
            "Букет невесты",
            "Деталь праздничного стола",
            "Самый красивый аксессуар"),

        new(
            "emotions",
            "Эмоции",
            "Слёзы счастья",
            "Искренняя улыбка",
            "Волнение",
            "Восторг"),

        new(
            "selfie",
            "Селфи",
            "Селфи с молодожёнами",
            "Групповое селфи",
            "Зеркальное селфи",
            "Самое неожиданное селфи"),

        new(
            "toasts",
            "Тосты",
            "Лучший тост",
            "Бокалы вместе",
            "Реакция на тост",
            "Фото после тоста"),

        new(
            "newlyweds",
            "Молодожёны",
            "Дарья и Янис вместе",
            "Счастливый взгляд",
            "Молодожёны с гостями",
            "Тайный момент молодожёнов"),

        new(
            "style",
            "Стиль",
            "Самый стильный гость",
            "Лучший свадебный образ",
            "Красивые туфли",
            "Модная деталь"),

        new(
            "surprise",
            "Сюрпризы",
            "Неожиданный подарок",
            "Удивлённое лицо",
            "Секретный момент",
            "Неожиданный гость"),

        new(
            "children",
            "Дети",
            "Самый юный гость",
            "Детский смех",
            "Маленький танцор",
            "Дети и молодожёны"),

        new(
            "flowers",
            "Цветы",
            "Свадебный букет",
            "Цветочная деталь",
            "Гость с цветами",
            "Самый красивый цветок"),

        new(
            "food",
            "Угощения",
            "Свадебный торт",
            "Самое красивое блюдо",
            "Сладкий момент",
            "Праздничные бокалы"),

        new(
            "memories",
            "Воспоминания",
            "Давние друзья",
            "Фото из прошлого",
            "Повторите старую фотографию",
            "История в одном кадре"),

        new(
            "group",
            "Все вместе",
            "Все гости",
            "Все девочки",
            "Все мальчики",
            "Общее праздничное фото"),

        new(
            "random",
            "Случайность",
            "Фото с закрытыми глазами",
            "Первое, что вы увидите",
            "Кадр без подготовки",
            "Сфотографируйте незнакомого гостя")
    ];

    public static WeddingBoard GenerateBoard(
        IEnumerable<string> selectedTagKeys)
    {
        var selectedKeys = selectedTagKeys
            .Distinct(StringComparer.OrdinalIgnoreCase)
            .ToHashSet(StringComparer.OrdinalIgnoreCase);

        var matchingTags = Tags
            .Where(tag => selectedKeys.Contains(tag.Key))
            .ToList();

        var prompts = matchingTags
            .SelectMany(tag => tag.Prompts)
            .Concat(UniversalPrompts)
            .Distinct(StringComparer.OrdinalIgnoreCase)
            .OrderBy(_ => Random.Shared.Next())
            .Take(12)
            .ToList();

        var slots = prompts
            .Select((prompt, index) =>
                new PhotoSlot(
                    index + 1,
                    prompt,
                    LayoutClasses[index]))
            .ToList();

        return new WeddingBoard
        {
            CoupleNames = "Дарья и Янис",
            WeddingDate = "19.09.2026",
            BoardTitle = "Ваш фотоквест",
            BoardDescription =
                "Добавьте фотографии во все задания.",
            Slots = slots
        };
    }
}