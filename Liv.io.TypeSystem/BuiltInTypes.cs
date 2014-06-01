using System;

namespace Liv.io.TypeSystem
{
	public static class BuiltInTypes
	{
		public static T Any {
			get {
				return _any;
			}
		}

		private static readonly T _any = new T (Context.SystemContext, "any", Guid.Parse ("49a0e9cf-a7d4-47f2-836a-6037fde5f1ba"));

		public static T Bool {
			get {
				return _bool;
			}
		}

		private static readonly T _bool = new T (Context.SystemContext, "bool", Guid.Parse ("527ed31f-d15d-4fa7-8439-3ab10dbd86ca"));

		public static T Int {
			get {
				return _int;
			}
		}

		private static readonly T _int = new T (Context.SystemContext, "int", Guid.Parse ("ce38950f-bf84-4a83-b042-0b6dcb08a6f8"));

		public static T Float {
			get {
				return _float;
			}
		}

		private static readonly T _float = new T (Context.SystemContext, "float", Guid.Parse ("1869b298-9a18-4b3f-9ebc-e2aff52351c4"));

		public static T Date {
			get {
				return _date;
			}
		}

		private static readonly T _date = new T (Context.SystemContext, "date", Guid.Parse ("22470516-ba9d-4232-ad80-7bc04321d390"));

		public static T String {
			get {
				return _string;
			}
		}

		private static readonly T _string = new T (Context.SystemContext,"string", Guid.Parse ("f31d4447-6f5e-40a8-b3e1-a544eaa36e83"));
	}
}