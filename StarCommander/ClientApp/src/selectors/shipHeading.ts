import * as R from "ramda";

export default R.pathOr<number>(0, ["ship", "heading"]);
