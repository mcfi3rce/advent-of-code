package aoc;

import aoc.util.Input;
import java.nio.file.Files;
import java.nio.file.Path;
import java.util.stream.IntStream;

/** Usage: Main [day ...]. With no args, runs every day that has a class. */
public class Main {
    public static void main(String[] args) throws Exception {
        int[] days = args.length > 0
                ? java.util.Arrays.stream(args).mapToInt(Integer::parseInt).toArray()
                : IntStream.rangeClosed(1, 25).filter(Main::exists).toArray();
        for (int day : days) {
            run(day);
        }
    }

    private static boolean exists(int day) {
        try {
            Class.forName(className(day));
            return true;
        } catch (ClassNotFoundException e) {
            return false;
        }
    }

    private static String className(int day) {
        return String.format("aoc.days.Day%02d", day);
    }

    private static void run(int day) throws Exception {
        Day solver = (Day) Class.forName(className(day)).getDeclaredConstructor().newInstance();
        String input = Input.read(day);
        System.out.printf("== Day %02d ==%n", day);
        time("Part 1", () -> solver.part1(input));
        time("Part 2", () -> solver.part2(input));
    }

    private static void time(String label, java.util.function.Supplier<String> part) {
        long start = System.nanoTime();
        String answer = part.get();
        long ms = (System.nanoTime() - start) / 1_000_000;
        System.out.printf("%s: %s (%d ms)%n", label, answer, ms);
    }
}
