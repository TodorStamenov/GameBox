class ResponseModel {
  bool success;
  String? message;
  dynamic data;

  ResponseModel({
    required this.success,
    this.message,
    this.data,
  });
}
