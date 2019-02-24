﻿public interface IPoolable
{
    void Init();
    void Pick();
    bool IsBeingUsed();
    void Return();
}
