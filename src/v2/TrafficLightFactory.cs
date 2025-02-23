namespace TrafficLight;
public static class Factory {
    public static ITrafficLight GetTrafficLight(string? type) {
        return type switch
        {
            TrafficLightTypes.pedestrian => new Pedestrian(),
            TrafficLightTypes.intermitent => new Intermitent(),
            _ => new TrafficLightVehicle()
        };
    }
}
