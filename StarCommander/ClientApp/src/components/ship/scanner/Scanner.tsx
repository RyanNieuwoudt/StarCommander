import React from "react";
import { useSelector } from "react-redux";
import { Chart } from "grommet";
import { scanCoordinates } from "selectors";

export default function Scanner() {
	const values = useSelector(scanCoordinates) as any;

	return <Chart round type="point" values={values} />;
}
