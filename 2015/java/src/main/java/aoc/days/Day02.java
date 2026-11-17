package aoc.days;

import java.util.Arrays;
import java.util.List;

import aoc.Day;

public class Day02 implements Day {
    @Override
    public String part1(String input) {
        // (length l, width w, and height h)
        // 2*l*w + 2*w*h + 2*h*l
        List<String> lines = input.lines().toList(); // Java 16+
        var total = 0;
        for (String line : lines) {

            var values = line.split("x");
            int[] ints = Arrays.stream(values)
                    .mapToInt(Integer::parseInt)
                    .toArray();
            var l = ints[0];
            var w = ints[1];
            var h = ints[2];

            // We need to get the sides but we can't multiply by 2 until 
            int[] sides = { l * w, w * h, h * l};
            var smallest = Arrays.stream(sides).min().getAsInt();
            var multiplied = Arrays.stream(sides).map(x -> x * 2).toArray();

            total += Arrays.stream(multiplied).sum() + smallest;
        }

        return String.valueOf(total);
    }

    @Override
    public String part2(String input) {
        return "TODO";
    }
}
