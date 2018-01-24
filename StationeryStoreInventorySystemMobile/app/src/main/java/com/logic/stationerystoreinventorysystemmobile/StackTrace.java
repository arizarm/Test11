package com.logic.stationerystoreinventorysystemmobile;

import java.io.PrintWriter;
import java.io.StringWriter;

/**
 * Created by April Wang on 20/1/2018.
 */

public class StackTrace {
    public static String trace(Exception ex) {
        StringWriter outStream = new StringWriter();
        ex.printStackTrace(new PrintWriter(outStream));
        return outStream.toString();
    }
}