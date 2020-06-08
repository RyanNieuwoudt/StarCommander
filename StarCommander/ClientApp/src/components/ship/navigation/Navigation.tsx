import * as R from "ramda";
import React from "react";
import { useSelector } from "react-redux";
import styled from "styled-components/macro";
import { shipPosition } from "selectors";
import { Coordinate } from ".";

const Circle = styled.div`
	background-color: lightgray;
	border-radius: 50%;
	position: relative;
`;

export default function Navigation() {
	const position = useSelector(shipPosition);

	const x = R.prop("x", position as any);
	const y = R.prop("y", position as any);

	return (
		<Circle>
			<Coordinate axis="x" value={x} />
			<Coordinate axis="y" value={y} />
		</Circle>
	);
}
