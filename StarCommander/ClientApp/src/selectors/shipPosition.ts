import * as R from "ramda";
import { Position } from "store/ship";

export default R.path<Position>(["ship", "position"]);
