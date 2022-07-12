package com.example.demo;

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

    @GetMapping("/long")
    public String Long() throws InterruptedException {
        Thread.sleep(300);
        return "All good";
    }
   
}
