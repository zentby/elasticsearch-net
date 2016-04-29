﻿using System;

namespace Nest
{
	public abstract class CorePropertyDescriptorBase<TDescriptor, TInterface, T>
		: PropertyDescriptorBase<TDescriptor, TInterface, T>, ICoreProperty
		where TDescriptor : CorePropertyDescriptorBase<TDescriptor, TInterface, T>, TInterface
		where TInterface : class, ICoreProperty
		where T : class
	{
		bool? ICoreProperty.Store { get; set; }
		SimilarityOption? ICoreProperty.Similarity { get; set; }
		Fields ICoreProperty.CopyTo { get; set; }
		IProperties ICoreProperty.Fields { get; set; }

		protected CorePropertyDescriptorBase(string type) : base(type) {}

		public TDescriptor Store(bool store = true) => Assign(a => a.Store = store);

		public TDescriptor Fields(Func<PropertiesDescriptor<T>, IPromise<IProperties>> selector) => Assign(a => a.Fields = selector?.Invoke(new PropertiesDescriptor<T>())?.Value);

		public TDescriptor Similarity(SimilarityOption similarity) => Assign(a => a.Similarity = similarity);

		public TDescriptor CopyTo(Func<FieldsDescriptor<T>, IPromise<Fields>> fields) => Assign(a => a.CopyTo = fields?.Invoke(new FieldsDescriptor<T>())?.Value);
	}
}
