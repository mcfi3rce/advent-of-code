package aoc.days;

import static org.junit.jupiter.api.Assertions.assertEquals;

import aoc.util.Samples;
import org.junit.jupiter.api.Test;

class Day05Test {
    // Fill these in with the answers from the puzzle text for src/test/resources/samples/day05.txt.
    private static final String PART1_SAMPLE_ANSWER = "?";
    private static final String PART2_SAMPLE_ANSWER = "?";

    private final Day05 day = new Day05();
    private final String sample = Samples.read(5);

    @Test
    void part1Sample() {
        assertEquals(PART1_SAMPLE_ANSWER, day.part1(sample));
    }

    @Test
    void part2Sample() {
        assertEquals(PART2_SAMPLE_ANSWER, day.part2(sample));
    }
}
