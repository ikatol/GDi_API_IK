namespace GDi_API_IK.Model {
    public class ResponseCodes {
        public enum Code {
            SUCCESS,
            INTERNAL_ERROR,
            DATA_VALIDATION_ERROR,
            NOT_FOUND
        }

        public static int GetHttpCode(Code code) {
            switch(code) {
                case Code.SUCCESS: return 200;
                case Code.INTERNAL_ERROR: return 500;
                case Code.DATA_VALIDATION_ERROR: return 500;
                case Code.NOT_FOUND: return 404;
                default: return 500;
            }
        }
    }
}
