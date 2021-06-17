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

	return (
		<Circle>
			{position && (
				<>
					<Coordinate axis="x" value={position.x} />
					<Coordinate axis="y" value={position.y} />
				</>
			)}
		</Circle>
	);
}
