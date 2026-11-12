package aoc.util;

import java.io.IOException;
import java.io.UncheckedIOException;
import java.nio.file.Files;
import java.nio.file.Path;
import java.util.List;

public final class Input {
    private Input() {}

    /** Reads inputs/dayNN.txt with trailing whitespace trimmed. */
    public static String read(int day) {
        Path path = Path.of("inputs", String.format("day%02d.txt", day));
        try {
            return Files.readString(path).strip();
        } catch (IOException e) {
            throw new UncheckedIOException("Missing input file: " + path, e);
        }
    }

    public static List<String> lines(String input) {
        return input.strip().lines().toList();
    }
}
