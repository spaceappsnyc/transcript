using System;
using System.Collections.Generic;
using Newtonsoft.Json;

public class SegmentContainer
{
	[JsonProperty ("TextItems")]
	public List<Segment> Segments { get; set; }
}

public class Segment
{
	// miliseconds
	[JsonProperty ("Begin")]
	public float Begin { get; set; }

	// miliseconds
	[JsonProperty ("End")]
	public float End { get; set; }

	[JsonProperty ("Quote")]
	public string Text { get; set; }
}
