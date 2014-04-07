using UnityEngine;
using System.Collections.Generic;

namespace GLWireframe {
	public class RingBuffer<T> {
		
		T[] buffer;
		
		int first;
		int last;
		int mask;
		
		public RingBuffer(int capacity)
		{
			capacity = NextPowerOfTwo((uint)capacity);
			buffer = new T[capacity];
			mask = capacity - 1;
		}
		
		public void Push(T value) {
			while(last - first >= buffer.Length) { // dont re allocate
				first++; // the same as pop
			}
			this[last] = value;
			last++;
		}
		
		public T Pop() {
			if(last - first <= 0) {
				throw new System.IndexOutOfRangeException();
			}
			T i = this[first];
			first++;
			return i;
		}
		
		public T this [int index] {
			get {
				if(index < first || last <= index) {
					throw new System.IndexOutOfRangeException(string.Format("first:{0} index:{1} last:{2}", first, index, last));
				}
				return buffer[index & mask];
			}
			protected set {
				buffer[index & mask] = value;
			}
		}
		
		public IEnumerator<T> GetEnumerator()
		{
			for (int i=first; i<last; ++i) {
				yield return this[i];
			}
		}
		
		int NextPowerOfTwo(uint n) {
			--n;
			int p = 0;
			for (; n != 0; n >>= 1) p = (p << 1) + 1;
			return p + 1;
		}
	}
}
