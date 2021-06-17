import * as R from "ramda";

export default R.pathSatisfies(Boolean, ["auth", "token"]);
