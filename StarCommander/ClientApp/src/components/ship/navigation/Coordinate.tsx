import React from "react";
import { Text } from "grommet";
import styled from "styled-components/macro";

const X = styled(Text)`
	left: 80%;
	position: absolute;
	top: 50%;
`;

const Y = styled(Text)`
	left: 50%;
	position: absolute;
	top: 10%;
`;

interface CoordinateProps {
	value: number | undefined;
	axis: "x" | "y";
}

export default function Coordinate(props: CoordinateProps) {
	const { axis, value } = props;

	if (!value) {
		return null;
	}

	if (axis === "x") {
		return <X>{value}</X>;
	}
	return <Y>{value}</Y>;
}
