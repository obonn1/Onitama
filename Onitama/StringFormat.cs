namespace Onitama
{
    public static class StringFormats
    {
        public static StringFormat Center { get; } = new() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };

        public static StringFormat CenterTop { get; } = new() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Near };

        public static StringFormat CenterBottom { get; } = new() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Far };
    }
}
