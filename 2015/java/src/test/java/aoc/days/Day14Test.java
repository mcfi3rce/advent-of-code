package aoc.days;

import static org.junit.jupiter.api.Assertions.assertEquals;

import aoc.util.Samples;
import org.junit.jupiter.api.Test;

class Day14Test {
    // Fill these in with the answers from the puzzle text for src/test/resources/samples/day14.txt.
    private static final String PART1_SAMPLE_ANSWER = "?";
    private static final String PART2_SAMPLE_ANSWER = "?";

    private final Day14 day = new Day14();
    private final String sample = Samples.read(14);

    @Test
    void part1Sample() {
        assertEquals(PART1_SAMPLE_ANSWER, day.part1(sample));
    }

    @Test
    void part2Sample() {
        assertEquals(PART2_SAMPLE_ANSWER, day.part2(sample));
    }
}
