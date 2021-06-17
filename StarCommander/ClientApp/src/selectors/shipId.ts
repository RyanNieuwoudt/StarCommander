import * as R from "ramda";

export default R.pathOr<string>("", ["ship", "shipId"]);
