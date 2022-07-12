package com.example.demo;

import java.util.Random;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.RestController;

@RestController
public class WeatherController {


    @Autowired
    private DotNetCallService dotNetCall;

    @GetMapping("/weather")
    public Weather weather() {
        Weather[] data = dotNetCall.getWeather();

        return data[0];
    }

    private Random rand = new Random();

    @GetMapping("/long")
    public String Long() throws InterruptedException {
        if(rand.nextBoolean()) {
            Thread.sleep(1);
        }
        return "All good";
    }
   
}
