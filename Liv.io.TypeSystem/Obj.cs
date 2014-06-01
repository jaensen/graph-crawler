using System;
using System.Linq;
using System.Collections.Generic;

namespace Liv.io.TypeSystem
{
	/// <summary>
	/// Represents any object which may be created by a user.
	/// </summary>
	public sealed class Obj : IList<KeyValuePair<T,Obj>>
	{
		List<KeyValuePair<T, Obj>> _slots;

		/// <summary>
		/// Initializes a new instance of the <see cref="Liv.io.Types.Obj"/> class.
		/// </summary>
		/// <param name="isLiteral">If set to <c>true</c> this object is a literal value.</param>
		public Obj (bool isLiteral = false)
		{
			_isLiteral = isLiteral;

			if (!_isLiteral)
				_slots = new List<KeyValuePair<T, Obj>> ();
		}

		/// <summary>
		/// Gets a value indicating whether this instance is a literal.
		/// </summary>
		/// <value><c>true</c> if this instance is a literal; otherwise, <c>false</c>.</value>
		public bool IsLiteral {
			get {
				return _isLiteral;
			}
		}

		private readonly bool _isLiteral;

		/// <summary>
		/// If this is a literal <see cref="IsLiteral"/> then this property describes the type of the literal.
		/// </summary>
		/// <value>The type of the literal.</value>
		public T LiteralType {
			get;
			set;
		}

		/// <summary>
		/// If this is a literal <see cref="IsLiteral"/> then this property contains the literal value.
		/// </summary>
		/// <value>The literal value.</value>
		public object LiteralValue {
			get;
			set;
		}

		private void ThrowIsLiteralException ()
		{
			if (IsLiteral)
				throw new InvalidOperationException ("This object is a literal and can therefore not contain other objects or be iterated");
		}

		public int IndexOf (KeyValuePair<T, Obj> item)
		{
			ThrowIsLiteralException ();
			return _slots.IndexOf (item);
		}

		public void Insert (int index, KeyValuePair<T, Obj> item)
		{
			ThrowIsLiteralException ();
			_slots.Insert (index, item);
		}

		public void RemoveAt (int index)
		{
			ThrowIsLiteralException ();
			_slots.RemoveAt (index);
		}

		public KeyValuePair<T, Obj> this [int index] {
			get {
				if (IsLiteral && index != 0)
					throw new ArgumentOutOfRangeException ("index", "The object is a literal and therefore only has one value. The literal value.");
				return _slots [index];
			}
			set {
				if (IsLiteral && index != 0)
					throw new ArgumentOutOfRangeException ("index", "The object is a literal and therefore only has one value. The literal value.");
				_slots [index] = value;
			}
		}

		public void Add (KeyValuePair<T, Obj> item)
		{
			ThrowIsLiteralException ();
			_slots.Add (item);
		}

		public void Clear ()
		{
			ThrowIsLiteralException ();
			_slots.Clear ();
		}

		public bool Contains (KeyValuePair<T, Obj> item)
		{
			ThrowIsLiteralException ();
			return _slots.Contains (item);
		}

		public void CopyTo (KeyValuePair<T, Obj>[] array, int arrayIndex)
		{
			ThrowIsLiteralException ();
			_slots.CopyTo (array, arrayIndex);
		}

		public bool Remove (KeyValuePair<T, Obj> item)
		{
			ThrowIsLiteralException ();
			return _slots.Remove (item);
		}

		public int Count {
			get {
				ThrowIsLiteralException ();

				return _slots.Count;
			}
		}

		public bool IsReadOnly {
			get {
				if (IsLiteral)
					return true;

				return false;
			}
		}

		public IEnumerator<KeyValuePair<T, Obj>> GetEnumerator ()
		{
			ThrowIsLiteralException ();
			return _slots.GetEnumerator ();
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator ()
		{
			ThrowIsLiteralException ();
			return this.GetEnumerator ();
		}
	}
}