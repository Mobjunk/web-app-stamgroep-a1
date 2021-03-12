var ImageUploaderPlugin = {
  ImageUploaderInit: function() {
    var fileInput = document.createElement('input');
    fileInput.setAttribute('type', 'file');
    fileInput.setAttribute('id', 'ImageUploaderInput');
    fileInput.style.visibility = 'hidden';
	fileInput.name = "image";
    fileInput.onclick = function (event) {
      this.value = null;
    };
    fileInput.onchange = function (event) {
		console.log("Handle the thingy for image selected " + event.target.files[0].name);
		SendMessage('Canvas', 'FileName', event.target.files[0].name);
		SendMessage('Canvas', 'FileSelected', URL.createObjectURL(event.target.files[0]));
    }
    document.body.appendChild(fileInput);
  }
};
mergeInto(LibraryManager.library, ImageUploaderPlugin);