import * as R from "ramda";
import { Position } from "store/ship";

//TODO TypeScript vs Ramda

export default R.compose(
	R.map((p: Position) => R.append(-p.y, [p.x])) as any,
	R.prop("scanner") as any
);
