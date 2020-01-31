import * as React from "react";
import { Box, Grid, Heading } from "grommet";
import { Layout } from "components";
import { Acknowledgements } from "components/ship/crew";
import { Helm } from "components/ship/helm";
import { Navigation } from "components/ship/navigation";
import { Scanner } from "components/ship/scanner";

export default function Bridge() {
	return (
		<Layout>
			<Box align="center" pad="large">
				<Heading>Welcome, Commander.</Heading>
			</Box>
			<Grid
				alignSelf="center"
				columns={[
					["medium", "full"],
					["medium", "full"]
				]}
				gap="medium"
				rows="auto"
			>
				<Helm />
				<Navigation />
				<Acknowledgements />
				<Scanner />
			</Grid>
		</Layout>
	);
}
