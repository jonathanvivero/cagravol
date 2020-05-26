using SGE.Cagravol.Presentation.ViewModel.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;


namespace SGE.Cagravol.Application.Core.Helpers
{
	public static class AssemblyHelpers
	{
		public static IEnumerable<Type> GetViewModelTypes<T>()
		{
			var types = Assembly.GetAssembly(typeof(T))
					   .GetTypes()
					   .Where(i => IsContained<T>(i) && i.IsAbstract == false)
					   .Distinct();

			return types;
		}

		public static Type GetViewModelByClass(string name)
		{
			var assembly = Assembly.GetAssembly(typeof(IEditViewModel<>));
			var type = assembly.GetTypes()
				.Where(i => i.Name == name)
				.SingleOrDefault();

			return type;
		}

		public static T GetInstanceOf<T>(string typename)
		{
			var viewModelType = AssemblyHelpers.GetViewModelByClass(typename);
			var concreteObject = (T)Activator.CreateInstance(viewModelType);
			return concreteObject;
		}


		private static bool IsContained<T>(Type type)
		{
			var typeFullName = typeof(T).Name;

			var types = GetClassHierarchy(type);
			var result = types
				.Where(i => i.Name == typeFullName)
				.Any();

			return result;
		}

		private static IEnumerable<Type> GetClassHierarchy(Type type)
		{
			while (type != null)
			{
				yield return type;
				type = type.BaseType;
			}
		}
	}
}
