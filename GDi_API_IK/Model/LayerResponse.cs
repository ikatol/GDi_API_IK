namespace GDi_API_IK.Model {
    public class LayerResponse<T> : LayerResponse {
        public T? Payload { get; set; }
    }

    public class LayerResponse {
        public bool Success { get; set; } = true;
        public ResponseCodes.Code ResponseCode { get; set; } = ResponseCodes.Code.SUCCESS;
        public string Message { get; set; } = string.Empty;
        public string ExMessage { get; set; } = string.Empty;
    }
}
