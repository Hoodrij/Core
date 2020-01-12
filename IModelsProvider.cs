using System;
using System.Collections.Generic;

public interface IModelsProvider
{
	IEnumerable<Type> Get();
}
