class Response {
  bool success;
  String? message;
  dynamic data;

  Response({
    required this.success,
    this.message,
    this.data,
  });
}
