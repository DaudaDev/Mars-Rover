const container = document.getElementById("plateau");

var plateau;
var rovers = [];

function makeRows(rows, cols) {
  container.style.setProperty("--grid-rows", rows + 1);
  container.style.setProperty("--grid-cols", cols + 1);
  for (c = cols; c >= 0; c--) {
    for (d = 0; d <= rows; d++) {
      var id = `cell-${d}-${c}`;
      var element = document.getElementById(id);
      if (element) {
        continue;
      }
      let cell = document.createElement("div");
      cell.id = id;
      cell.innerText = `${d} : ${c}`;
      container.appendChild(cell).className = "grid-item";
    }
  }
}

function printRoverPositions() {
  var uploadButton = document.getElementById("getmovements");
  var resetButton = document.getElementById("reset");
  resetButton.disabled = true;
  uploadButton.disabled = true;
  timoutIncrement = 1;
  this.rovers.forEach(function (rover, roverIndex) {
    var color = "#" + (((1 << 24) * Math.random()) | 0).toString(16);
    rover.previousMovements.forEach(async function (
      roverPosition,
      index,
      array
    ) {
      setTimeout(
        () =>
          moveRover(
            roverPosition,
            index > 0 ? array[index - 1] : false,
            color,
            roverIndex
          ),
        1000 * timoutIncrement
      );
      timoutIncrement++;
    });
  });
  setTimeout(() => {
    uploadButton.disabled = false;
    resetButton.disabled = false;
  }, 1000 * timoutIncrement);

  function moveRover(roverPosition, previousPosition, color, roverIndex) {
    const getRotation = (direction) => {
      if (direction === "N") {
        return 0;
      } else if (direction === "S") {
        return 180;
      } else if (direction === "E") {
        return 90;
      } else if (direction === "W") {
        return 270;
      }
    };
    let cell = document.getElementById(
      `cell-${roverPosition.xAxis}-${roverPosition.yAxis}`
    );
    const title = `Position: ${roverPosition.xAxis}  ${roverPosition.yAxis} ${roverPosition.direction}`;
    cell.innerHTML = `<i class="pt-1 fa-2xl fa-solid fa-robot fa-rotate-${getRotation(
      roverPosition.direction
    )}"  style="color:${color}" title="${title}"></i>
        `;
    cell.style.backgroundColor = color + "22";
    cell.scrollIntoView();
    if (
      previousPosition &&
      (previousPosition.xAxis != roverPosition.xAxis ||
        previousPosition.yAxis != roverPosition.yAxis)
    ) {
      let previousCell = document.getElementById(
        `cell-${previousPosition.xAxis}-${previousPosition.yAxis}`
      );
      previousCell.style.backgroundColor = color + "33";
      previousCell.innerText = `${previousPosition.xAxis} : ${previousPosition.yAxis}`;
    }
    return cell;
  }
}
function resetPlateau() {
  container.innerHTML = "";
}
async function uploadFiles() {
  let movements = document.getElementById("fileUpload").files[0];

  if (!movements) {
    alert("Upload file clicking Get Movements");
    return;
  }

  let formData = new FormData();

  formData.append("movements", movements);

  const ctrl = new AbortController(); // timeout
  setTimeout(() => ctrl.abort(), 5000);

  try {
    let response = await fetch("https://localhost:5047/upload", {
      method: "POST",
      body: formData,
      signal: ctrl.signal,
    });
    if (!response.ok) {
      var error = await response.text();
      alert(error);
      return;
    }
    var result = await response.json();
    this.plateau = result.plateau;
    this.rovers = result.rovers;
    this.makeRows(this.plateau.upperY, this.plateau.upperX);
    this.printRoverPositions();
  } catch (e) {
    alert("Error:", e);
  }
}
