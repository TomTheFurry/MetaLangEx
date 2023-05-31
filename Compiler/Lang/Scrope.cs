using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.Lang;

public interface IDefinition
{
    public abstract IScrope Scrope { get; }
    public abstract string Name { get; }

}

public class Variable : IDefinition
{
    public IScrope Scrope { get; set; }
    public string Name { get; set; }

    public Clazz Clazz { get; set; }
}

public class Callable : IDefinition
{
    public IScrope Scrope { get; set; }
    public string Name { get; set; } 
}

public interface IScrope : IDefinition
{
    public HashSet<IDefinition> Definitions { get; set; }
    
    public IDefinition GetDefinition(string name);
}

public abstract class Size
{
    public abstract bool IsSolved();
    public abstract int Get();
}
public class ConstSize : Size
{
    public int Size { get; set; }

    public override int Get() => Size;
    public override bool IsSolved() => true;
}
public class ChainedSize : Size
{
    public int SizeOffset { get; set; }
    public List<Clazz> WithClazz { get; set; }

    public override int Get()
    {
        if (!IsSolved()) throw new InvalidOperationException();
        return SizeOffset + WithClazz.Sum(c => c.Size.Get());
    }

    public override bool IsSolved() => WithClazz.All(c => c.Size.IsSolved());
}

public class Clazz : IScrope
{


    public IScrope Scrope { get; set; }
    public HashSet<IDefinition> Definitions { get; set; }

    public string Name { get; set; }
    public Size Size { get; set; }

    public IDefinition GetDefinition(string name) => throw new NotImplementedException();


}
