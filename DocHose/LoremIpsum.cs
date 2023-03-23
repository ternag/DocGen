namespace DocHose;

public static class LoremIpsum
{
    private static readonly string _loremIpsum = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nullam nunc sapien, scelerisque vel pulvinar blandit, pellentesque vel arcu. Pellentesque posuere, enim sed vulputate sodales, turpis quam cursus arcu, sed dapibus felis mi at est. Quisque tincidunt pellentesque sollicitudin. Quisque rhoncus ac enim eu commodo. Praesent condimentum est ac purus lobortis, eget pretium metus volutpat. Cras ac quam mauris. Proin vestibulum nisi sed dui gravida ullamcorper. Pellentesque luctus ultrices urna, in finibus mauris. Curabitur ultricies ac ipsum bibendum aliquam. Orci varius natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Sed ac tortor dignissim, maximus velit a, consequat tortor. Morbi congue suscipit purus at lobortis. Ut neque tortor, tristique vel sagittis vel, placerat vitae nisi. Mauris ultrices tortor id commodo lobortis.";
    private static readonly string[] _words = _loremIpsum.Split(' ');

    public static string GetRandomLength()
    {
        Random random = new Random();
        int numberOfWords = random.Next(7, _words.Length);
        string lorem = string.Join(' ', _words.AsSpan(0, numberOfWords).ToArray());
        return lorem.EndsWith('.') ? lorem : lorem + ".";
    }
}