// Copyright (C) 2015 Theunis Swanepoel. <theunis.swanepoel@gmail.com>
// This software is licensed under GNU LGPLv3.

namespace Biscuits.Extensions
{
    using System;
    using System.Reactive.Linq;

    public static class ObservableExtensions
    {
        public static IObservable<TResult> CombineLatest<TSource1, TSource2, TResult>(this IObservable<TSource1> source1, IObservable<TSource2> source2, Func<TSource1, TSource2, bool, bool, TResult> resultSelector)
        {
            IObservable<Tuple<TSource1, bool>> alt1 = Observable.Scan(source1, Tuple.Create(default(TSource1), false), (accu, v) => Tuple.Create(v, !accu.Item2));
            IObservable<Tuple<TSource2, bool>> alt2 = Observable.Scan(source2, Tuple.Create(default(TSource2), false), (accu, v) => Tuple.Create(v, !accu.Item2));

            bool odd1 = false;
            bool odd2 = false;

            return Observable.CombineLatest(alt1, alt2, (v1, v2) =>
                {
                    TResult result = resultSelector(v1.Item1, v2.Item1, v1.Item2 != odd1, v2.Item2 != odd2);

                    odd1 = v1.Item2;
                    odd2 = v2.Item2;

                    return result;
                });
        }
    }
}
