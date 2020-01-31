import React from "react";
import { useSelector } from "react-redux";
import { Text } from "grommet";
import styled from "styled-components/macro";
import { shipPosition } from "selectors";

const Circle = styled.div`
	background-color: lightgray;
	border-radius: 50%;
	position: relative;
`;

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

export default function Navigation() {
	const position = useSelector(shipPosition);

	if (!position) {
		return null;
	}

	const { x, y } = position;

	return (
		<Circle>
			<X>{x}</X>
			<Y>{y}</Y>
		</Circle>
	);
}
