var UnityImageUploader = {
  ImageUploaderCaptureClick: function() {
    document.getElementById('ImageUploaderInput').click();
  }
};
mergeInto(LibraryManager.library, UnityImageUploader);