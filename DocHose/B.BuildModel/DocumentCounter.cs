namespace DocHose.B.BuildModel;

public class DocumentCounter
{
    private int _id = 1;
    public int GetNextId() => _id++;
}