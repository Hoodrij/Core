using System;
using System.Collections.Generic;
using Core.Abstract;

namespace Core
{
	public interface IServicesProvider
	{
		IEnumerable<AService> Get();
	}
}