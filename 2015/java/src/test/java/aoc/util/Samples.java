package aoc.util;

import java.io.IOException;
import java.io.InputStream;
import java.io.UncheckedIOException;
import java.nio.charset.StandardCharsets;

/** Loads sample inputs from src/test/resources/samples/dayNN.txt. */
public final class Samples {
    private Samples() {}

    public static String read(int day) {
        String name = String.format("/samples/day%02d.txt", day);
        try (InputStream in = Samples.class.getResourceAsStream(name)) {
            if (in == null) {
                throw new IllegalStateException("Missing sample file: src/test/resources" + name);
            }
            return new String(in.readAllBytes(), StandardCharsets.UTF_8).strip();
        } catch (IOException e) {
            throw new UncheckedIOException(e);
        }
    }
}
