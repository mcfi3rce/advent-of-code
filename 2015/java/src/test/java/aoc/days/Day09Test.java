package aoc.days;

import static org.junit.jupiter.api.Assertions.assertEquals;

import aoc.util.Samples;
import org.junit.jupiter.api.Test;

class Day09Test {
    // Fill these in with the answers from the puzzle text for src/test/resources/samples/day09.txt.
    private static final String PART1_SAMPLE_ANSWER = "?";
    private static final String PART2_SAMPLE_ANSWER = "?";

    private final Day09 day = new Day09();
    private final String sample = Samples.read(9);

    @Test
    void part1Sample() {
        assertEquals(PART1_SAMPLE_ANSWER, day.part1(sample));
    }

    @Test
    void part2Sample() {
        assertEquals(PART2_SAMPLE_ANSWER, day.part2(sample));
    }
}
