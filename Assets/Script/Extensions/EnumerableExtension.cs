using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public static class EnumerableExtensions {
    public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source) {
        return source.Shuffle(new Random());
    }

    public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source, Random rng) {
        if(source == null)
            throw new ArgumentNullException("Source");
        if(rng == null)
            throw new ArgumentNullException("Random Number Generator");

        return source.ShuffleIterator(rng);
    }

    private static IEnumerable<T> ShuffleIterator<T>(this IEnumerable<T> source, Random rng) {
        List<T> buffer = source.ToList();

        for(int i = 0; i < buffer.Count; i++) {
            int j = rng.Next(i, buffer.Count);
            yield return buffer[j];
            buffer[j] = buffer[i];
        }
    }
}