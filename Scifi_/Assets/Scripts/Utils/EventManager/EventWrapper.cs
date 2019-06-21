
//Wrapper for no return types
public class EventWrapper : IEventWrapper {

}

//For one return type
public class EventWrapper<DataType> : IEventWrapper {
    public DataType dataType;

    public EventWrapper(DataType dataType) {
        this.dataType = dataType;
    }
}

public class EventWrapper<DataType1, DataType2> : IEventWrapper {
    public DataType1 dataType1;
    public DataType2 dataType2;

    public EventWrapper(DataType1 dataType1, DataType2 dataType2) {
        this.dataType1 = dataType1;
        this.dataType2 = dataType2;
    }
}