package aoc.days;

import static org.junit.jupiter.api.Assertions.assertEquals;

import aoc.util.Samples;
import org.junit.jupiter.api.Test;

class Day01Test {
    private static final String PART1_SAMPLE_ANSWER = "-1";
    private static final String PART2_SAMPLE_ANSWER = "5";

    private final Day01 day = new Day01();
    private final String sample = Samples.read(1);

    @Test
    void part1Sample() {
        assertEquals(PART1_SAMPLE_ANSWER, day.part1(sample));
    }

    @Test
    void part2Sample() {
        assertEquals(PART2_SAMPLE_ANSWER, day.part2(sample));
    }
}
