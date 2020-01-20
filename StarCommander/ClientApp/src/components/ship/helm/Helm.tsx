import React, { useCallback, useState } from "react";
import { useDispatch, useSelector } from "react-redux";
import { Box, Button, Heading as HeadingText } from "grommet";
import { shipId as shipIdSelector } from "selectors";
import { actionCreators } from "store/ship";
import { Heading, Speed } from ".";

interface SetNumber {
	target: { value: React.SetStateAction<number> };
}

export default function Helm() {
	const dispatch = useDispatch();

	const shipId = useSelector(shipIdSelector);

	const [newHeading, setNewHeading] = useState(0);
	const [newSpeed, setNewSpeed] = useState(0);

	const engage = useCallback(() => {
		if (!shipId) {
			return;
		}

		dispatch(actionCreators.setHeading(shipId, newHeading));
		dispatch(actionCreators.setSpeed(shipId, newSpeed));
	}, [newHeading, newSpeed]);

	return (
		<Box>
			<HeadingText level={2}>Helm</HeadingText>
			<Heading
				value={newHeading}
				onChange={(event: SetNumber) =>
					setNewHeading(event.target.value)
				}
			/>
			<Speed
				value={newSpeed}
				onChange={(event: SetNumber) => setNewSpeed(event.target.value)}
			/>
			<Button label="Engage" primary onClick={engage} />
		</Box>
	);
}
