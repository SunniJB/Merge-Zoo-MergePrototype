using System;

namespace tusj.Services {

public interface ISavable {
    public void Write(object data);
    public object Read();
    public string GetKey();
}

}