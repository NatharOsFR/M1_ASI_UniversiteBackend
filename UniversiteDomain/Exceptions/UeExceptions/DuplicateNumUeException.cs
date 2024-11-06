namespace UniversiteDomain.Exceptions.EtudiantExceptions;

[Serializable]
public class DuplicateNumUeException : Exception
{
    public DuplicateNumUeException() : base() { }
    public DuplicateNumUeException(string message) : base(message) { }
    public DuplicateNumUeException(string message, Exception inner) : base(message, inner) { }
}